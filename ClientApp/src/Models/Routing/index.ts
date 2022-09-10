import { ComponentType } from "react";

export interface IRoutingProp {
  path: string;
  element: JSX.Element | ComponentType;
}

export interface IRouting {
  routes: Array<IRoutingProp>
}