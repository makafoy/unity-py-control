using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using AustinHarris.JsonRpc;
using UnityEngine;

class NetworkEvent {
	// contains request from client, space for reply from server,
	// and a thread signaling thing so client can wait for reply
	// we'll keep everything as json strings here, and do the conversion
	// in the main thread
	public string clientRequest;
	public string? serverReply;
	public AutoResetEvent serverReplied = new AutoResetEvent(false);
	public NetworkEvent(string clientRequest) {
		this.clientRequest = clientRequest;
	}
}



public class NetManager : MonoBehaviour {
	[Tooltip("Network port to listen on.")]
	public int ListenPort = 9000;
	[Tooltip("Network address to listen on. If not sure, put 'localhost'")]
	public string ListenAddress = "localhost";
	[Tooltip("Optional filepath to write logs to.")]
	public string? LogFilepath;
	[Tooltip("milliseconds to sleep after writing to logfile. Helps prevent runaway logs")]
	public int SleepAfterLogLineMilliseconds = 100;
	[Tooltip("Ensure the application runs in background. If you set this to false, you might wonder why your python scripts appears to hang")]
	public bool AutoEnableRunInBackground = true;

	bool blockingListen = false;
	bool isDedicated;
	volatile bool isEnabled = true;

	HttpListener? listener;
	BlockingCollection<NetworkEvent> networkEvents = new BlockingCollection<NetworkEvent>();

	void MyDebug(string msg) {
		if(LogFilepath != null && LogFilepath != "") {
			string DateTime = System.DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff");
			using(StreamWriter sw = File.AppendText(LogFilepath)) {
				sw.WriteLine($"{DateTime} {msg}");
			}
			Thread.Sleep(SleepAfterLogLineMilliseconds);
		}
	}

	class RpcService : JsonRpcService {
		// this is only for turning blocking on/off
		NetManager netManager;
		public RpcService(NetManager netManager) {
			this.netManager = netManager;
		}
		[JsonRpcMethod]
		void shutdownUnity() {
			// This is intended for dedicatd servers that are spawned by UnityComms
			Debug.Log("received shutdownUnity()");
			Application.Quit();
			Debug.Log("After application.quit()");
			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			Debug.Log("after setting isPlaying to false");
			#endif
			System.Diagnostics.Process.GetCurrentProcess().Kill();
			Debug.Log("after get current process . kill. hopefully we never get here :)");
		}
		[JsonRpcMethod]
		private void setBlockingListen(bool blocking)
		{
			// if blocking is true, then we listen for requests from python
			// using a blocking listen. This will run faster than non-blocking,
			// but if the python stops sending, then the editor will freeze, and the only
			// solution (other than restarting a python sender), is to Force Quit the Unity
			// Editor.
			this.netManager.SetBlocking(blocking);
		}
	}

	RpcService? rpcService;

	private void Awake() {
		rpcService 