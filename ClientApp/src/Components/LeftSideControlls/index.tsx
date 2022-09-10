import React, { useEffect, useState } from "react";
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

  const getValue = async (value:string,setFunc:React.Dispatch<any>) => {
    console.log('getValue',value);
    const response = await geoCodingService.getForwardCoding(value)
    if(response){
      const { results } = response
      if(results.length > 0){
        const result = results.map<{ label:string,value:[number,number] }>((value) => {
          const  { city,country,house_number,road } =  value.components
          const label = [ city,road,house_number ].filter(v => v !== undefined).join(", ")
          return { label, value:[ value.geometry.lat, value.geometry.lng ] }
        })
        return setFunc(result)
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
          placeholder="Откуда"
          onChange={(value: string) => {
            if (value?.length >= 3) {
              getValue(value,setDotAData)
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
          placeholder="Куда"
          onChange={(value: string) => {
            console.log(value);
            if (value?.length >= 3) {
              getValue(value,setDotBData)
            }
          }}
          onSelected={(option) => setDotState(prev => ({ ...prev,dotB:option }))}
          defVal={dotState.dotB?.label}
          options={dotBData}
        />
        <Button
          size="large"
          disabled={dotState.dotA === undefined || dotState.dotB === undefined}
          className="rounded-lg text-lime-50 disabled:text-zinc-200 disabled:bg-zinc-400 bg-lime-400 hover:bg-lime-500 hover:border-lime-100 hover:text-black focus:bg-lime-400 focus:border-lime-100 focus:text-lime-500"
        >
          Заказать такси
        </Button>
      </div>
    </div>
  );
};

export default LeftSideControls;
