import { FC } from 'react';
import { IAuthorizedProps } from '../../Models';

const AuthorizedView : FC<IAuthorizedProps> = ({ children } : IAuthorizedProps) => {

  return (
    <>
      { children }
    </>
  )
}

export default AuthorizedView;
