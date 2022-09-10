import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import React from 'react';
import { INotificationState, SwaggerException } from '../../Models';
import { BaseUrl, RequestService } from '../Request';

function subscribe(sub: PushSubscription): Promise<Observable<any>> {
  let url_ = (BaseUrl + "/api/Notification/subscribe").replace(/[?&]$/, "");
  let content_ = JSON.stringify(sub);

  return RequestService.post(url_, content_).then(_observableMergeMap((response_: any) => {
    return processSubscribe(response_);
  })).catch(_observableCatch((response_: any) => {
    if (response_) {
      try {
        return processSubscribe(response_);
      }
      catch (e) {
        return _observableThrow(e);
      }
    }
    else
      return _observableThrow(response_);
  }));
}

function processSubscribe(response: any): Observable<any> {
  const status = response.status;
  const responseBlob = response ? response.body : (response).error instanceof Blob ? (response).error : undefined;
  let _headers: any = {};
  if (response.headers) {
    for (let key of response.headers.keys()) {
      _headers[key] = response.headers.get(key);
    }
  };
  if (status === 200) {
    return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
      return _observableOf(null);
    }));
  }
  else if (status !== 200 && status !== 204) {
    return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
      return throwException("An unexpected server error occurred.", status, _responseText, _headers);
    }));
  }
  return _observableOf(null);
}

function unsubscribe(sub: PushSubscription): Promise<Observable<any>> {
  let url_ = (BaseUrl + "/api/Notification/unsubscribe").replace(/[?&]$/, "");
  let content_ = JSON.stringify(sub);

  return RequestService.post(url_, content_).then(_observableMergeMap((response_: any) => {
    console.log(response_);
    return processUnsubscribe(response_);
  })).catch(_observableCatch((response_: any) => {
    if (response_) {
      try {
        console.log(response_);
        return processUnsubscribe(response_);
      }
      catch (e) {
        console.log(e);
        return _observableThrow(e);
      }
    }
    else {
      console.log(response_);
      return _observableThrow(response_);
    }
  }));
}

function processUnsubscribe(response: any): Observable<any> {
  const status = response.status;
  const responseBlob = response ? response.body : response.error instanceof Blob ? response.error : undefined;
  let _headers: any = {};
  if (response.headers) {
    for (let key of response.headers.keys()) {
      _headers[key] = response.headers.get(key);
    }
  };
  if (status === 200) {
    return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
      return _observableOf(null);
    }));
  }
  else if (status !== 200 && status !== 204) {
    return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
      return throwException("An unexpected server error occurred.", status, _responseText, _headers);
    }));
  }
  return _observableOf(null);
}

function blobToText(blob: any): Observable<string> {
  return new Observable<string>((observer: any) => {
    if (!blob) {
      observer.next("");
      observer.complete();
    } else {
      let reader = new FileReader();
      reader.onload = function () {
        observer.next(this.result);
        observer.complete();
      }
      reader.readAsText(blob);
    }
  });
}

function urlB64ToUint8Array(base64String: string) {
  const padding = '='.repeat((4 - base64String.length % 4) % 4);
  const base64 = (base64String + padding)
    .replace(/\-/g, '+')
    .replace(/_/g, '/');

  const rawData = window.atob(base64);
  const outputArray = new Uint8Array(rawData.length);

  for (let i = 0; i < rawData.length; ++i) {
    outputArray[i] = rawData.charCodeAt(i);
  }
  return outputArray;
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
  if (result !== null && result !== undefined)
    return _observableThrow(result);
  else
    return _observableThrow(new SwaggerException(message, status, response, headers, null));
}

export const NotificationContext = React.createContext<INotificationState>({
  swRegistration: undefined,
  isSubscribed: false,
  isSupported: false,
  isInProgress: false
});

const applicationServerPublicKey = "BNS33EUbavImWuaI0ps0ISNAxHEpQkHbyAo11Yl4IPiVP78b4LiQ29s_Sp7LP4s0wO68ztAO9t8k9zkuBB-S_9M";

class NotificationService {

  constructor(public notificationState: INotificationState) {

  }

  async Init() {
    if (this.notificationState.swRegistration == undefined && 'serviceWorker' in navigator && 'PushManager' in window) {
      await navigator.serviceWorker.register('sw.js')
        .then(async swReg => {
          console.log('Service Worker is registered', swReg);

          this.notificationState.swRegistration = swReg;
          await this.CheckSubscription();
        })
        .catch(error => {
          console.error('Service Worker Error', error);
        });
      this.notificationState.isSupported = true;
    }
    else {
      this.notificationState.isSupported = false;
    }
  }
  async CheckSubscription() {
    await this.notificationState.swRegistration?.pushManager.getSubscription()
      .then(subscription => {
        this.notificationState.isSubscribed = !(subscription === null);
      })
  }
  async SubscribeUser() {
    this.notificationState.isInProgress = true;
    await this.notificationState.swRegistration?.pushManager.subscribe({
      userVisibleOnly: true,
      applicationServerKey: urlB64ToUint8Array(applicationServerPublicKey)
    }).
      then((subscription: any) => {
        let newSub = JSON.parse(JSON.stringify(subscription));
        (subscribe({
          // @ts-ignore
          auth: newSub.keys.auth,
          p256Dh: newSub.keys.p256dh,
          endPoint: newSub.endpoint
        }))
          .then(s => {
            this.notificationState.isSubscribed = true;
            console.log('User subscribed.');
          })
      })
      .catch((err: any) => {
        console.log('Failed to subscribe the user: ', err);
      })
      .then(() => {
        this.notificationState.isInProgress = false;
      });
  }
  async UnsubscribeUser(callback: () => void) {
    this.notificationState.isInProgress = true;
    let sub: any;
    await this.notificationState.swRegistration?.pushManager.getSubscription()
      .then(subscription => {
        if (subscription) {
          sub = JSON.parse(JSON.stringify(subscription));
          console.log('User unsubscribed');
          return subscription.unsubscribe();
        }
      })
      .catch(function (error: any) {
        console.log('Error unsubscribing', error);
      })
      .then(() => {
        unsubscribe({
          // @ts-ignore
          auth: sub.keys.auth,
          p256Dh: sub.keys.p256dh,
          endPoint: sub.endpoint
        });
        this.notificationState.isSubscribed = false;
        this.notificationState.isInProgress = false;
        callback();
      });
  }
};

export { NotificationService };