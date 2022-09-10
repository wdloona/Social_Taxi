import React, { useState } from 'react';

const ChatInput = (props: any) => {

  const [message, setMessage] = useState('');

  const onSubmit = (e: any) => {
    e.preventDefault();

    const isMessageProvided = message && message !== '';

    if (isMessageProvided) {
      props.sendMessage(message);
    }
    else {
      alert('Введите сообщение!');
    }
  }

  const onMessageUpdate = (e: any) => {
    setMessage(e.target.value);
  }

  return (
    <form
      onSubmit={onSubmit}>
      <label htmlFor="message">Сообщение:</label>
      <br />
      <input
        type="text"
        id="message"
        name="message"
        value={message}
        onChange={onMessageUpdate} />
      <br /><br />
      <button>Отправить</button>
    </form>
  )
};

export default ChatInput;