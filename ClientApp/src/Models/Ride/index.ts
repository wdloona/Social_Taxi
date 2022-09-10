import { addressModel } from "../Address";

interface rideModelBase {
  address: addressModel;
  seatsCount: number;
  beginDate: Date;
  endDate?: Date;
  description: string
}

interface rideModel extends rideModelBase {
  rideId: number;
  creatorUserId: number;
  seatsCount: number;
  endDate: Date;
  flActive: boolean;
  flFinished: boolean;
  description: string
}


export type { rideModelBase ,rideModel }