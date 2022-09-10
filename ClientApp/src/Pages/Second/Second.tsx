import Chat from '../../Components/Chat/Chat';
import { CookieService } from '../../Services';

function Second() {

  let userId = CookieService.get("UserId");

  return (
    <div>
      <Chat toUserId={userId == "6" ? "1":"6" } />
    </div>
  );
}

export default Second;