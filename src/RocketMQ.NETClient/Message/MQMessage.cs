/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

using System;
using System.Runtime.InteropServices;
using System.Text;
using RocketMQ.NetClient.Interop;

namespace RocketMQ.NetClient.Message
{
    public class MQMessage : IMessage
    {
        #region default Options  
        private HandleRef _handleRef;
        private string MessageBody = "default message body";
        private string MessageTags = "default_tags";
        #endregion

        #region Constructor
        private void MessageInit(string topic) {
            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new ArgumentException(nameof(topic));
            }

            var handle = MessageWrap.CreateMessage(topic);
            this._handleRef = new HandleRef(this, handle);

            var result = MessageWrap.SetMessageTopic(this._handleRef, topic);
            if (result != 0)
            {
                MessageWrap.DestroyMessage(this._handleRef);
                throw new Exception($"set message topic error. cpp sdk return code: {result}");
            }
            this.SetMessageBody(this.MessageBody);
            this.SetMessageTags(this.MessageTags);
        }
        public MQMessage(string topic)
        {
            this.MessageInit(topic);
        }
        public MQMessage(string topic,string messageBody,string messageTags){
            this.MessageInit(topic);
            this.SetMessageBody(messageBody);
            this.SetMessageTags(MessageTags);
        }

        #endregion

        #region Get 
        public HandleRef GetHandleRef() {
            return this._handleRef;
        }
        #endregion

        #region Set Message API
        public void SetMessageTopic(string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                MessageWrap.DestroyMessage(this._handleRef);
                throw new ArgumentException(nameof(topic));
            }
            
            var result = MessageWrap.SetMessageTopic(this._handleRef, topic);
            if (result != 0)
            {
                MessageWrap.DestroyMessage(this._handleRef);
                throw new Exception($"set message topic error. cpp sdk return code: {result}");
            }
            
            return ;
        }

        public void SetMessageTags(string tags)
        {
            if (string.IsNullOrWhiteSpace(tags))
            {
                MessageWrap.DestroyMessage(this._handleRef);
                throw new ArgumentException(nameof(tags));
            }

            var result = MessageWrap.SetMessageTags(this._handleRef, tags);
            if (result != 0)
            {
                MessageWrap.DestroyMessage(this._handleRef);
                throw new Exception($"set message tags error. cpp sdk return code: {result}");
            }
            
            return ;
        }

        public void SetMessageKeys(string keys)
        {
            if (string.IsNullOrWhiteSpace(keys))
            {
                MessageWrap.DestroyMessage(this._handleRef);
                throw new ArgumentException(nameof(keys));
            }

            var result = MessageWrap.SetMessageKeys(this._handleRef, keys);
            if (result != 0)
            {
                MessageWrap.DestroyMessage(this._handleRef);
                throw new Exception($"set message keys error. cpp sdk return code: {result}");
            }
            
            return;
        }

        public void SetMessageBody(string body)
        {
            if (string.IsNullOrWhiteSpace(body))
            {
                MessageWrap.DestroyMessage(this._handleRef);
                throw new ArgumentException(nameof(body));
            }

            var result = MessageWrap.SetMessageBody(this._handleRef, body);
            if (result != 0)
            {
                MessageWrap.DestroyMessage(this._handleRef);
                throw new Exception($"set message body error. cpp sdk return code: {result}");
            }
            
            return ;
        }
        /// <summary>
        /// todo 
        /// </summary>
        /// <param name="body"></param>
        //public void SetByteMessageBody(byte[] body)
        //{
        //    if (body == null || body.Length == 0)
        //    {
        //        MessageWrap.DestroyMessage(this._handleRef);
        //        throw new ArgumentException(nameof(body));
        //    }

        //    var byteBody = Encoding.UTF8.GetString(body);
        //    var result = MessageWrap.SetByteMessageBody(this._handleRef, byteBody, byteBody.Length);
        //    if (result != 0)
        //    {
        //        MessageWrap.DestroyMessage(this._handleRef);
        //        throw new Exception($"set message body error. cpp sdk return code: {result}");
        //    }
            
        //    return ;
        //}

        public void SetMessageProperty(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                MessageWrap.DestroyMessage(this._handleRef);
                throw new ArgumentException(nameof(key));
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                MessageWrap.DestroyMessage(this._handleRef);
                throw new ArgumentException(nameof(value));
            }

            var result = MessageWrap.SetMessageProperty(this._handleRef, key, value);
            if (result != 0)
            {
                MessageWrap.DestroyMessage(this._handleRef);
                throw new Exception($"set message property error. cpp sdk return code: {result}");
            }
            
            return ;
        }
       
        public void SetDelayTimeLevel(int level)
        {
            if (level < 0)
            {
                MessageWrap.DestroyMessage(this._handleRef);
                throw new ArgumentOutOfRangeException(nameof(level));
            }
            
            var result = MessageWrap.SetDelayTimeLevel(this._handleRef, level);
            if (result != 0)
            {
                MessageWrap.DestroyMessage(this._handleRef);
                throw new Exception($"set delay time level error. cpp sdk return code: {result}");
            }
            
            return ;
        }
        #endregion
        
        public void Dispose()
        {
            if (this._handleRef.Handle != IntPtr.Zero)
            {
                MessageWrap.DestroyMessage(this._handleRef);
                this._handleRef = new HandleRef(null, IntPtr.Zero);
                GC.SuppressFinalize(this);
            }
        }

    }
}
