using RocketMQ.NetClient.Interop;
using RocketMQ.NetClient.Producer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Producer
{
    class Program
    {
        private static ProducerWrap.QueueSelectorCallback _queueSelectorCallback = new ProducerWrap.QueueSelectorCallback(
           (size, message, args) =>
           {
               Console.WriteLine($"size: {size}, message: {message}, ptr: {args}");

               return 0;
           });

        static void Main(string[] args)
        {
            Console.Title = "Producer";

            Console.WriteLine("Start create producer.");

            MQProducer producer = new MQProducer("GroupA", "127.0.0.1:9876");
            producer.StartProducer();

            try {
                while (true) {
                    // message
                    var message = MessageWrap.CreateMessage("test");
                    Console.WriteLine("message intPtr:" + message);

                    var p1 = new Program();
                    var messageIntPtr = new HandleRef(p1, message);

                    var setMessageBodyResult = MessageWrap.SetMessageBody(messageIntPtr, "hello" + Guid.NewGuid());
                    Console.WriteLine("set message body result:" + setMessageBodyResult);

                    var setTagResult = MessageWrap.SetMessageTags(messageIntPtr, "tag_test");
                    Console.WriteLine("set message tag result:" + setTagResult);

                    var setPropertyResult = MessageWrap.SetMessageProperty(messageIntPtr, "key1", "value1");
                    Console.WriteLine("set message property result:" + setPropertyResult);

                    // var setByteMessageBodyResult = MessageWrap.SetByteMessageBody(messageIntPtr, "byte_body", 9);
                    // Console.WriteLine("set byte message body result:" + setByteMessageBodyResult);


                    // SendMessageSync
                    //var sendResult = producer.SendMessageSync(messageIntPtr);
                    //Console.WriteLine("send result:" + sendResult + ", msgId: " + sendResult.MessageId);

                    // SendMessageOneway
                    //var sendResult = producer.SendMessageOneway(messageIntPtr);
                    //Console.WriteLine("send result:" + sendResult);

                    // SendMessageOneWay
                    var sendResult = producer.SendMessageOrderly(messageIntPtr,_queueSelectorCallback,"aa");
                    Console.WriteLine("send result:" + sendResult.MessageId);

                    //SendMessage

                    // var sendResult = producer.SendMessageAsync(
                    //     messageIntPtr,
                    //     result =>
                    //     {
                    //         Console.WriteLine($"success_callback_msgId: {result.msgId}");
                    //     },
                    //     ex =>
                    //     {
                    //         Console.WriteLine($"error_callback_msgId: {ex.msg}");
                    //     }
                    // );
                    //Console.WriteLine("send result:" + sendResult);

                    // var pArgs = "args_parameters";
                    // var ptrArgs = Marshal.StringToBSTR(pArgs);
                    // var sendResult = ProducerWrap.SendMessageOrderly(producer, messageIntPtr, _queueSelectorCallback,
                    //     ptrArgs, 1, out var sendResultStruct);
                    // Console.WriteLine($"send result:{sendResult}, sendResultStruct -> msgId: {sendResultStruct.msgId}, status: {sendResultStruct.sendStatus}, offset: {sendResultStruct.offset}");

                    Thread.Sleep(500);
                }
                var shutdownResult =producer.ShutdownProducer();
                Console.WriteLine("shutdown result:" + shutdownResult);

                var destoryResult = producer.DestroyProducer();
                Console.WriteLine("destory result:" + destoryResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadKey(true);
        }
    }
}
