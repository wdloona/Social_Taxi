import { addressModel, rideModel, rideModelBase, } from "../../Models";
import { RequestService } from "../Request";


const RideService = {
  GetRidesById: (userId: number): Promise<rideModel[]> => RequestService.get('Ride?userId=' + userId),
  GetRidesByAddress: (address: addressModel): Promise<rideModel[]> => RequestService.post('Ride/byaddress', address),
  AddRide: (ride: rideModelBase): Promise<number> => RequestService.post('Ride', ride),
  EditRide: (ride: rideModel): Promise<number> => RequestService.put('Ride', ride),
  DeleteRide: (rideId: number): Promise<number> => RequestService.delete('Ride?rideId=' + rideId),
};

export { RideService };

