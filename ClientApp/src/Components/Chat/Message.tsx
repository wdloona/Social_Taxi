import React from 'react';

const Message = (props: { from: boolean; message: string; }) => (
  <div style={{ background: "#eee", borderRadius: '5px', padding: '0 10px' }}>
    <p style={{ textAlign: props.from ? 'left' : 'right' }}>{props.message}</p>
  </div>
);;

export default Message;