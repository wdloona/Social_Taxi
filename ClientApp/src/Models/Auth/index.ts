import { ReactNode , ReactElement } from "react";
import AuthorizedView from "../../Components/AuthView/AuthorizedView";
import NonAuthorizedView from "../../Components/AuthView/NonAuthorizedView";

type AuthUser = typeof AuthorizedView;
type NonAuthUser = typeof NonAuthorizedView;

export interface IAuthorizedProps {
  children?:ReactNode
} 

export interface INonAuthorizedProps {
  children?:ReactNode
}

export interface NonAuthorizedViewState {

}

export interface AuthViewProps {
  children: ReactElement<AuthUser>
}