# .NET API 参考

[toc]

##  MQProducer API

### (构造函数) 

*  **Functions**

```C#
 MQProducer(string groupName, DiagnosticListener diagnosticListener = null);
 
 MQProducer(string groupName,string nameServerAddress, DiagnosticListener diagnosticListener = null);

 MQProducer(string groupName, string nameServerAddress,string logPath, DiagnosticListener diagnosticListener = null)

 MQProducer(string groupName, string nameServerAddress, string logPath,LogLevel logLevel, DiagnosticListener diagnosticListener = null)


```

*  **Consruct MQProducer**

构造一个MQProducer对象

* **Parameters**
`groupName`

    生产者对应的组
`nameServerAddress`

    nameServer 地址
`logPath` 

    日志路径
`logLevel`

   日志级别 

### 设置相关参数

#### SetProducerNameServerAddress

```C#
 void SetProducerNameServerAddress(string nameServerAddress)
```
*  **SetNameServerAddress**
    设置`Producer`的地址
* **Parameters**
`nameServerAddress`
    nameServer 地址



#### SetProducerNameServerDomain

```C#
 void SetProducerNameServerDomain(string nameServerDomain)
```

* **SetProducerNameServerDomain**
    使用Domain设置`Producer`的地址
* **Parameters**
`nameServerDomain`

    nameServer Domain

#### SetProducerGroupName


```C#
 void SetProducerGroupName(string groupName)
```

* **SetProducerGroupName**
    设置生产者的组号
* **Parameters**
`groupName`
    生产者对应的组



#### SetProducerInstanceName

```C#
 void SetProducerInstanceName(string instanceName)
```

* **SetProducerInstanceName**
   设置生产者实例名称
* **Parameters**
`instanceName`
   生产者的实例名称


#### SetProducerSessionCredentials

```C#
void SetProducerSessionCredentials(string accessKey, string secretKey, string onsChannel)
```

* **SetProducerSessionCredentials**
   设置生产者的认证信息，链接阿里云rocektmq时必要设置参数
* **Parameters**
`accessKey`
   阿里云账户 `accessKey`
`secretKey`
   阿里云账户 `secretKey`
`onsChannel`
  一般默认值为 `ALIYUN`

#### SetProducerLogPath

```C#
void SetProducerLogPath(string logPath)
```

* **SetProducerLogPaths**
   设置生产者的日志路径
* **Parameters**
`logPath`
   日志路径

####  SetProducerLogFileNumAndSize

```C#
void SetProducerLogFileNumAndSize(int fileNum, long fileSize)
```

* **SetProducerLogFileNumAndSize**
   设置生产者的日志文件数量和大小
* **Parameters**
`fileNum`
   日志文件数量
`fileSize`
   日志文件大小

#### SetProducerLogLevel

```C#
void SetProducerLogLevel(LogLevel logLevel)
```

* **SetProducerLogFileNumAndSize**
   设置生产者的日志文件数量和大小
* **Parameters**
`logLevel`
  枚举日志级别（详见LogLeveL）

#### SetAutoRetryTimes

```C#
void SetAutoRetryTimes(int times)
```

* **SetAutoRetryTimes**
   设置生产者发送消息失败时，重试次数
* **Parameters**
`times`
  生产者发送消息失败时，重试次数

### 发送消息API

#### SendMessageSync

```C#
SendResult SendMessageSync(HandleRef message)

```
* **SendMessageSync**
   同步消息发送
* **Parameters**
`message`
  消息handleRef

* **return**
    SendResult 对象（详见SendResult）

#### SendMessageOneway

```C#
SendResult SendMessageOneway(HandleRef message)

```
* **SendMessageOneway**
   单向消息发送
* **Parameters**
`message`
  消息handleRef

* **return**
    SendResult 对象（详见SendResult）

#### SendMessageOrderly

```C#
SendResult SendMessageOrderly(HandleRef message, QueueSelectorCallback callback, string args = "")

```
* **SendMessageOrderly**
   顺序消息发送
* **Parameters**
`message`
  消息handleRef
`callback`
  回调函数
`args`
  参数
* **return**
    SendResult 对象（详见SendResult）

## MQPushConsumer

### （构造函数）
*  **Functions**

```C#
MQPushConsumer(string groupId)
 
MQPushConsumer(string groupId, string nameServerAddress)

MQPushConsumer(string groupId, string nameServerAddress, string logPath, LogLevel logLevel)

```

*  **Consruct MQPushConsumer**

构造一个MQProducer对象

* **Parameters**
`groupName`

    消费者对应的组
`nameServerAddress`
    nameServer 地址
`logPath` 

    日志路径
`logLevel`

   枚举值，日志级别 

### 设置参数API


#### SetPushConsumerGroupId

```C#
void SetPushConsumerGroupId(string groupId)
```
* **SetPushConsumerGroupId**
   设置PushConsumer的组
* **Parameters**
`groupId`
    组号


#### SetPushConsumerNameServerAddress

```C#
void SetPushConsumerNameServerAddress(string nameServerAddress)
```
* **SetPushConsumerNameServerAddress**
   设置PushConsumer的组
* **Parameters**
`nameServerAddress`
nameServer地址信息

#### SetPushConsumerNameServerDomain

```C#
void SetPushConsumerNameServerDomain(string domain)
```
* **SetPushConsumerNameServerDomain**
   设置PushConsumer的NameServer Domain
* **Parameters**
`nameServerAddress`
nameServer地址信息