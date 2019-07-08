using RocketMQ.NetClient.Interop;
using RocketMQ.NetClient.Message;
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
                    MQMessage message = new MQMessage("test");

                    //var setPropertyResult = MessageWrap.SetMessageProperty(messageIntPtr, "key1", "value1");
                    //Console.WriteLine("set message property result:" + setPropertyResult);

                   


                    // SendMessageSync
                    //var sendResult = producer.SendMessageSync(message);
                    //Console.WriteLine("send result:" + sendResult + ", msgId: " + sendResult.MessageId);

                    // SendMessageOneway
                    //var sendResult = producer.SendMessageOneway(message);
                    //Console.WriteLine("send result:" + sendResult);

                    // SendMessageOneWay
                    var sendResult = producer.SendMessageOrderly(message.GetHandleRef(),_queueSelectorCallback,"aa");
                    Console.WriteLine("send result:" + sendResult.MessageId);

                    

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
