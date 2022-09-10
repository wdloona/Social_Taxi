import { useEffect, useState } from "react";
import { Button } from "antd";
import { GeoIcon } from "../CustomIcon";
import { AutoCompleteInput } from "./Components";
import { useRecoilState } from "recoil";
import { infoAboutRideState } from "../../Atoms";
import { geoCodingService } from "../../Services";





const LeftSideControls = () => {
  const [ dotState, setDotState ] = useRecoilState(infoAboutRideState);
  const [ dotAData, setDotAData ] = useState<Array<{ label:string,value:[number,number] }> | undefined>(undefined);
  const [ dotBData, setDotBData ] = useState<Array<{ label:string,value:[number,number] }> | undefined>(undefined);
  // useEffect(() => {
  //   console.log(dotState);
  // }, [dotState]);

  const getValue = async (value:string) => {
    const response = await geoCodingService.getForwardCoding(value)
    if(response){
      const { results } = response
      if(results.length > 0){
        const result = results.map<{ label:string,value:[number,number] }>((value) => {
          return { label:value.formatted , value:[ value.geometry.lat, value.geometry.lng ] }
        })
        return setDotAData(result)
      }
    }
  }

  return (
    <div className="flex flex-col">
      <div className="w-full h-fit py-5 bg-white rounded-lg flex flex-col gap-y-10 justify-center px-3">
        <AutoCompleteInput
          prefixInput={
            <span className="flex justify-center items-center gap-x-1 mr-2">
              <GeoIcon className="text-red-400" /> A
            </span>
          }
          placeholder="Куда"
          onChange={(value: string) => {
            console.log(value);
            if (value?.length >= 3) {
              getValue(value)
            }
          }}
          onSelected={(option) => setDotState(prev => ({ ...prev,dotA:option }))}
          defVal={dotState.dotA?.label}
          options={dotAData}
        />
        <AutoCompleteInput
          prefixInput={
            <span className="flex justify-center items-center gap-x-1 mr-2">
              <GeoIcon className="text-red-400" /> B
            </span>
          }
          placeholder="Откуда"
          onChange={(value: any) => {
            // setDotState(prev => ({ ...prev, dotB:value }))
          }}
        />
        <Button
          size="large"
          disabled={dotState.dotA === undefined || dotState.dotB === undefined}
          className="rounded-lg disabled:bg-zinc-400 bg-yellow-300 hover:bg-yellow-400 hover:border-yellow-100 hover:text-black focus:bg-yellow-400 focus:border-yellow-100 focus:text-black"
        >
          Заказать такси
        </Button>
      </div>
    </div>
  );
};

export default LeftSideControls;
