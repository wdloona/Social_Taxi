import { useState, FC, useEffect } from "react"
import { AuthViewProps, INotificationState } from "../../Models";
import { NotificationContext } from "../../Services";
import { LoginService } from "../../Services/Login";
import AuthorizedView from "./AuthorizedView";
import NonAuthorizedView from "./NonAuthorizedView";

const AuthView: FC<AuthViewProps> = ({ children }: AuthViewProps) => {

  const AuthCheck = async (): Promise<boolean> => {
    let result = await LoginService.getToken();
    if (result != "") {
      return true;
    }
    return false;
  }

  const [isAuthorized, setIsAuthorized] = useState<boolean | undefined>(undefined);
  const notificationState: INotificationState = {
    swRegistration: undefined,
    isSubscribed: false,
    isSupported: false,
    isInProgress: false
  };

  useEffect(() => {
    AuthCheck().then(r => setIsAuthorized(r))
  }, []);

  return (
    <NotificationContext.Provider value={notificationState}>
      <>
        {
          isAuthorized !== undefined ?
            (
              <>
                {
                  isAuthorized === true ?
                    <AuthorizedView>
                      {children}
                    </AuthorizedView>
                    :
                    <NonAuthorizedView />
                }
              </>
            ) : <></>
        }
      </>
    </NotificationContext.Provider >
  )
}

export default AuthView;
