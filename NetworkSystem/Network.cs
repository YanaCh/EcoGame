using System.Collections;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Network : MonoBehaviour
{
    public GamaController gameController;
    public static TcpClient client;
    public static int state = 0;

    int userId;
    string host = "46.101.164.179";
    string localHost = "192.168.1.5";
    int port = 6105;
    int localPort = 9090;

    NetworkStream stream;
    StreamWriter writer;
    StreamReader reader;


    

    bool initialized = false;

    private void Start()
    {
        StartNetwork();
        Ready();
    }

    public void ConnectToLocalServer()
    {
        if (client != null)
            client.Close();
        client = new TcpClient(localHost, localPort);
        Debug.Log("Connected to Local");
    }

    public void ConnectToServer()
    {
        if (client != null)
            client.Close();
        client = new TcpClient(host, port);
        Debug.Log("Connected to Inet");
    }

    public void StartNetwork()
    {
        Setup();
        StartCoroutine(CheckForMessages());
    }

    void Setup()
    {
        if (client == null)
        {
            try
            {
                //ConnectToLocalServer();
                ConnectToServer();
            }
            catch (SocketException e)
            {
                print(e.Message);
                //GameObject.Find("serverImg").GetComponent<Image>().color = new Color(1, 0, 0, 1);
                return;
            }
        }

        stream = client.GetStream();
        writer = new StreamWriter(stream);
        reader = new StreamReader(stream);
        initialized = true;
    }

    public void WriteSocket(string msg)
    {
        if (!initialized)
            return;
        writer.Write(msg);
        writer.Flush();
    }

    public string ReadSocket()
    {
        if (!initialized)
            return "";
        if (stream.DataAvailable)
        {
            byte[] data = new byte[1024];
            int bytes = stream.Read(data, 0, data.Length);
            string msg = System.Text.Encoding.UTF8.GetString(data, 0, bytes);
            Debug.Log(msg);
            return msg;
        }

        return "";
    }

    public void CloseSocket()
    {
        if (!initialized)
            return;
        writer.Close();
        reader.Close();
        client.Close();
        initialized = false;
    }


    public IEnumerator CheckForMessages()
    {
        while (true)
        {
            string response = ReadSocket();
            string[] commandsStack = response.Split(';');


            foreach (string cmd in commandsStack)
            {
                if (cmd.Length < 1)
                    continue;
                string[] data = cmd.Split(':');

                //InGame check
                if (data[0] == "start")
                {
                    gameController.uIController.ToggleWaitScreen(false);
                    gameController.StartGame();
                }

                if (data[0] == "end")
                {
                    gameController.uIController.SetEndGameScreen(gameController.score, int.Parse(data[1]));
                }
            }

            yield return new WaitForSeconds(0.3f);
        }

        //yield return new WaitForSeconds(0.3f);
    }

    //Commands
    //Pregame

    public void Login(string name, string password)
    {
        WriteSocket("login:" + name + ":" + password);
    }

    //Game

    public void Ready()
    {
        WriteSocket("rdy:;");
    }

    public void End(int score)
    {
        WriteSocket("set:" + score + ";");
    }

    public void OnApplicationQuit()
    {
        WriteSocket("closeConnection:;");
    }
}
