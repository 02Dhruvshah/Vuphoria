using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

/// <summary> 
/// TCP Client-Server Connection Example.
/// 2 way communication: Client (your app) connects to the server and can send and receive messages.
/// Simply place this script on an empty gameobject in your scene. Note your firewall may need to be disabled.
/// The OrderCompleteNewPhone method is an example of how you could use it. e.g calling it from a UI button.
/// <summary> 

public class TCP_Message : MonoBehaviour
{
	#region private members 	
	private TcpClient socketConnection;
	private Thread clientReceiveThread;
	private string subString;
	#endregion

	#region public members 	

	// Address of the server PC connected to the Festo machine. This should not need to be changed.
	public string serverAddress = "172.21.0.90"; 

	// Example of a message to send to the MES server.
	// This message places a new order of the complete mobile phone (known as part number 210). See documentation on Canvas for full breakdown of how this string is formatted.
	// Additional part numbers can be found on the Festo PC.   
	public string newOrderMessage = "444;RequestID=0;MClass=101;MNo=2;ErrorState=0;#PNo=210;#Aux1Int=1\r";
	
	public int currentOrderNumber; //Ono
	public int currentOrderPosition; //Opos
	public int currentProductNumberOfOrderNumber; //WPNo
	public int currentOrderPartNumber; //PNo
	public int currentStepNumber; //StepNo

	#endregion
	// Use this for initialization 	
	void Start()
	{
		ConnectToTcpServer();
	}

	/// <summary> 	
	/// Setup socket connection. 	
	/// </summary> 	
	private void ConnectToTcpServer()
	{
		try
		{
			clientReceiveThread = new Thread(new ThreadStart(ListenForData));
			clientReceiveThread.IsBackground = true;
			clientReceiveThread.Start();
		}
		catch (Exception e)
		{
			Debug.Log("On client connect exception " + e);
		}
	}
	/// <summary> 	
	/// Runs in background clientReceiveThread; Listens for incoming data. 	
	/// </summary>     
	private void ListenForData()
	{
		try
		{
			socketConnection = new TcpClient(serverAddress, 2000);
			Byte[] bytes = new Byte[1024];
			while (true)
			{
				// Get a stream object for reading 				
				using (NetworkStream stream = socketConnection.GetStream())
				{
					int length;
					// Read incoming stream into byte arrary. 					
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
					{
						var incomingData = new byte[length];
						Array.Copy(bytes, 0, incomingData, 0, length);
						// Convert byte array to string message. 						
						string serverMessage = Encoding.ASCII.GetString(incomingData);						
						
						// this is the message the MES server sends back. Its formatting is the same as the message you send to it. 
						Debug.Log("Server message received as: " + serverMessage);

						if (serverMessage.Length > 0)
							ExtractOrderInformation(serverMessage);

					}
				}
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}
	/// <summary> 	
	/// Send message to server using socket connection. 	
	/// </summary> 	
	private void SendMessageToServer(string message)
	{
		if (socketConnection == null)
		{
			return;
		}
		try
		{
			// Get a stream object for writing. 			
			NetworkStream stream = socketConnection.GetStream();
			if (stream.CanWrite)
			{
				string clientMessage = message;
				// Convert string message to byte array.                 
				byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
				// Write byte array to socketConnection stream.                 
				stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
				Debug.Log("Message has been sent by client - should be received by server");
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}

	/// <summary> 
	// Call this method from somewhere in your app to place a new order
	/// <summary> 
	public void OrderCompleteNewPhone()
	{
		SendMessageToServer(newOrderMessage);
		Debug.Log("new phone");
	}
	private void ExtractOrderInformation(string serverMessage)
    {
		string[] targets = { "ONo=", "OPos=", "WPNo=", "PNo=", "StepNo=" };
		int[] results = new int[targets.Length];
		int startPoint;

        for (int i = 0; i < targets.Length; i++)
        {
			startPoint = serverMessage.IndexOf(targets[i]);
			subString = serverMessage.Substring(startPoint + targets[i].Length, serverMessage.Length - startPoint - targets[i].Length);
			int result;
			int.TryParse(subString.Split(';')[0], out result);
			results[i] = result;
		}

		currentOrderNumber = results[0];
		currentOrderPosition = results[1];
		currentProductNumberOfOrderNumber = results[2];
		currentOrderPartNumber = results[3];
		currentStepNumber = results[4];

		Debug.Log($"Order: {currentOrderNumber} has been created for part number {currentOrderPartNumber}." +
			$" This order is in position {currentOrderPosition} and on step number {currentStepNumber}.");
	}

	  
	//ADD
	public void CustomOrder(int PNo)
	{
		string message = "444;RequestID=0;MClass=101;MNo=2;ErrorState=0;#PNo=" + PNo + ";#Aux1Int=1\r";
		SendMessageToServer(message);
		//"444;RequestID=0;MClass=101;MNo=2;ErrorState=0;#PNo=210;#Aux1Int=1\r"
		//Debug.Log("Order Placed " + "Specific Order number: " + PNo);
		Debug.Log(message);
	}
}