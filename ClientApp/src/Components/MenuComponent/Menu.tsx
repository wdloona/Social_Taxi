import React, { useContext } from 'react';
import { Link } from 'react-router-dom';
import { NotificationContext, NotificationService } from '../../Services';
import { LoginService } from '../../Services/Login';

const Menu = () => {
  const notifyContex = useContext(NotificationContext);
  
  const exit = async () => {
    let notificationService = new NotificationService(notifyContex);
    await notificationService.CheckSubscription();
    if (notifyContex.isSubscribed) {
      await notificationService.UnsubscribeUser(() => {
        logout();
      });
    }
    else
      logout();
  }

  const logout = () => {
    LoginService.logout();
    window.location.href = "/login";
  }

  return (
    <>
      <nav>
        <ul>
          <li>
            <Link to="/">Главная</Link>
          </li>
          <li>
            <Link to="/first">Первая</Link>
          </li>
          <li>
            <Link to="/second">Вторая</Link>
          </li>
        </ul>
      </nav>
      <button onClick={() => exit() }>Выйти</button>
    </>
  );
}
export default Menu;