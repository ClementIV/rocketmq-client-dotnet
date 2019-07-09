using RocketMQ.NetClient.Consumer;
using RocketMQ.NetClient.Interop;
using RocketMQ.NetClient.Message;
using RocketMQ.NETClient.Consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsumerQuickStart
{
    class Program
    {
        private static readonly PushConsumerWrap.MessageCallBack _callback = new PushConsumerWrap.MessageCallBack(HandleMessageCallBack);

        static void Main(string[] args)
        {
            Console.Title = "PushConsumer";

            Task.Run(() => {
                Console.WriteLine("start push consumer...");

                var consumer = new MQPushConsumer("xx", "127.0.0.1:9876");
                Console.WriteLine($"consumer: {consumer}");
                consumer.SetPushConsumerLogPath(".\\consumer_log.txt");
                consumer.SetPushConsumerLogLevel(LogLevel.Trace);

                var groupId = consumer.GetPushConsumerGroupID();
                Console.WriteLine($"groupId: {groupId}");
                consumer.Subscribe("test", "*");
                consumer.RegisterMessageCallback(_callback);
                var result=consumer.StartPushConsumer();
               
                Console.WriteLine($"start push consumer ptr: {result}");

                while (true)
                {
                    Thread.Sleep(500);
                }
            });
            Console.ReadKey(true);

            //PushConsumerBinder.DestroyPushConsumer(consumer);
        }

        public static int HandleMessageCallBack(IntPtr consumer, IntPtr message)
        {
            Console.WriteLine($"consumer: {consumer}; messagePtr: {message}");

            var body = MessageWrap.GetMessageBody(message);
            Console.WriteLine($"body: {body}");

            var messageId = MessageWrap.GetMessageId(message);
            Console.WriteLine($"message_id: {messageId}");

            return 0;
        }
    }
}
