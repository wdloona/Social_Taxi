import React from 'react';
import Message from './Message';

const ChatWindow = (props: { chat: { from: boolean, message: string }[]; }) => {

  const chat = props.chat
    .map(m =>
      <Message key={Date.now() * Math.random()}
        from={m.from}
        message={m.message} />
    );

  return (
    <div>
      {chat}
    </div>
  )
};

export default ChatWindow;