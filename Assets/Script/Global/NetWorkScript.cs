using System;
using System.Net.Sockets;
using UnityEngine;
//using static ByteArray;

public class NetWorkScript
{
    private static NetWorkScript instance;//为的就是全局使用同一个对象，这个对象实现消息的队列！开始时就是null
    private static Socket socket;
    private static string ip = "127.0.0.1";
    private static int port = 8083;
    private static byte[] buff = new byte[1024];//python是达不到这个效果的 static 隔离且持久
    public static NetWorkScript getInstance()//函数
    {
        if (instance == null)//避免进到这里来--必须在init里面初始化
        {
            init();
            Debug.Log("就怕次次初始化");//说对了！--避过去这个应该就不卡了！这个思维很超前的
        }
        return instance;
    }
    public static void init()//这个初始化函数一定要挂到按钮上去，两个按钮！索性就利用Start()函数
    {
        try
        {
            Debug.Log("66666666666666我估计这里就没有执行过666666666666666");
            instance = new NetWorkScript();//放在这里一次足够了--这里之后就不再是null了
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//一次就应该够用了，毕竟写过python的
            socket.Connect(ip, port);
            Debug.Log("success");
            socket.BeginReceive(buff, 0, 1024, SocketFlags.None, ReceiveCallBack, buff);//先收再回调
        }
        catch
        {
            Debug.Log("网络连接初始化faild");
        }
    }
    public static void 照猫画虎()//这个可是害死我了
    {
        Debug.Log("2");
        init();
        Debug.Log("3");
    }
    void Start()
    {
        Debug.Log("有Start的类也可以绑在画布上");
        init();
    }
    //sendmessage
    public void sendMessage(int type, int area, int command, string message)
    {

    }

    private static void ReceiveCallBack(IAsyncResult ar)//回调方法 
    {
        try
        {
            int readCount = 0;
            readCount = socket.EndReceive(ar);//ar其实就是传进来的内容
            byte[] temp = new byte[readCount];
            Buffer.BlockCopy(buff, 0, temp, 0, readCount);
            //再多新建一个
        }
        catch
        {
            socket.Close();
            Debug.Log("net error");
        }
        socket.BeginReceive(buff, 0, 1024, SocketFlags.None, ReceiveCallBack, buff);//形成闭环
    }
    private void readMessage(byte[] message)//处理所收到的信息
    {

    }

}