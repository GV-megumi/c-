using System;
using chapter;


public delegate void MyEventHandler(string message);

public class Publisher
{
    private MyEventHandler _myEvent;

    // 自定义事件访问器
    public event MyEventHandler MyEvent
    {
        add
        {
            Console.WriteLine("Custom logic before event subscription");
            _myEvent += value;
        }
        remove
        {
            Console.WriteLine("Custom logic before event unsubscription");
            _myEvent -= value;
        }
    }

    public void RaiseEvent(string message)
    {
        // 触发事件
        _myEvent?.Invoke(message);
    }
}

public class Subscriber
{
    public void HandleEvent(string message)
    {
        Console.WriteLine("Subscriber received the message: " + message);
    }
}

class chapter15_e : Chapter
{
            //chapter1-7
        public override void f()
        {
        Publisher publisher = new Publisher();
        Subscriber subscriber = new Subscriber();

        // 订阅事件
        publisher.MyEvent += subscriber.HandleEvent;

        // 发布者执行操作，触发事件
        publisher.RaiseEvent("Event is triggered from Publisher!");

        // 取消订阅事件
        publisher.MyEvent -= subscriber.HandleEvent;
    }
}
