import React, { useState, useEffect, useRef } from 'react';
import { HubConnection, HubConnectionBuilder, JsonHubProtocol } from '@microsoft/signalr';
import ChatWindow from './ChatWindow';
import ChatInput from './ChatInput';
import { BaseUrl, CookieService } from '../../Services';
import { LoginService } from '../../Services/Login';

const Chat = (props: any) => {

  const [connection, setConnection] = useState<HubConnection>();
  const [chat, setChat] = useState<{ from: boolean, message: string }[]>([]);
  const latestChat = useRef(chat);

  latestChat.current = chat;

  useEffect(() => {

    const options = {
      accessTokenFactory: () => CookieService.get("AccessToken")
    };

    const newConnection = new HubConnectionBuilder()
      .withUrl(`${BaseUrl}/hubs/chat`, options)
      .withHubProtocol(new JsonHubProtocol())
      .build();

    setConnection(newConnection);
  }, []);

  useEffect(() => {
    if (connection) {
      connection.start()
        .then(result => {
          console.log('Connected!');

          connection.on('ReceiveMessage', (userId: string, message: string) => {
            LoginService.getUser().then(response => {
              let currentUser = response.data;
              const updatedChat = [...latestChat.current];
              updatedChat.push({ from: userId != currentUser.userId.toString(), message });
              setChat(updatedChat);
            });
          });
        })
        .catch(e => console.log('Connection failed: ', e));
    }
  }, [connection]);

  const sendMessage = async (message: string) => {

    if (connection?.state === "Connected") {
      try {
        await connection.send('SendMessageToUser', props.toUserId, message);
      }
      catch (e) {
        console.log(e);
      }
    }
    else {
      alert('Не установлено соединение с сервером!');
    }
  }

  return (
    <div>
      <ChatInput sendMessage={sendMessage} />
      <hr />
      <ChatWindow chat={chat} />
    </div>
  );
};

export default Chat;