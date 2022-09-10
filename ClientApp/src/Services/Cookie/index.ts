import { ICookieProps } from "../../Models";

const CookieService = {
  get: (name: string): string => {
    let cookies: ICookieProps = Object.fromEntries(document.cookie.split(";").map(e => e.trim().split("=")));
    return cookies[name];
  }
};

export { CookieService };