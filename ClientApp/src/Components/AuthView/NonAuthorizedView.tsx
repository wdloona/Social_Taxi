import React from 'react'
import {Navigate, Route, Routes} from "react-router-dom";
import { INonAuthorizedProps, NonAuthorizedViewState } from '../../Models';
import Login from "../../Pages/Login/Login";

class NonAuthorizedView extends React.Component<INonAuthorizedProps, NonAuthorizedViewState> {
  constructor(props: INonAuthorizedProps) {
    super(props);
  }
  render() {
    return (
        <Routes>
          <Route path={"/"} element={<Login/>} />
          <Route path="*" element={<Navigate to={"/"} replace={true}/>}/>
        </Routes>
    );
  }
}

export default NonAuthorizedView;
