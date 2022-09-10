import React, { FC, useEffect, useState } from "react";
import { Button } from "antd";
import { GeoIcon } from "../CustomIcon";
import { AutoCompleteInput } from "./Components";
import { useRecoilState } from "recoil";
import { infoAboutRideState } from "../../Atoms";
import { geoCodingService } from "../../Services";
import Ride from "../Ride/Ride";





const LeftSideControls: FC<{ className: string }> = ({ className }) => {
  const [dotState, setDotState] = useRecoilState(infoAboutRideState);
  const [dotAData, setDotAData] = useState<Array<{ label: string, value: [number, number] }> | undefined>(undefined);
  const [dotBData, setDotBData] = useState<Array<{ label: string, value: [number, number] }> | undefined>(undefined);
  // useEffect(() => {
  //   console.log(dotState);
  // }, [dotState]);

  const getValue = async (value: string, setFunc: React.Dispatch<any>) => {
    console.log('getValue', value);
    const response = await geoCodingService.getForwardCoding(value)
    if (response) {
      const { results } = response
      if (results.length > 0) {
        const result = results.map<{ label: string, value: [number, number] }>((value) => {
          const { city, country, house_number, road } = value.components
          const label = [city, road, house_number].filter(v => v !== undefined).join(", ")
          return { label, value: [value.geometry.lat, value.geometry.lng] }
        })
        return setFunc(result)
      }
    }
  }

  return (
    <div className={"flex flex-col bg-white rounded-2xl " + className} >
      <div className="flex flex-col gap-2">

        <svg width="152" height="24" viewBox="0 0 380 60" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path className='fill-slate-900' d="M257.943 48.6527V59.4067H251.1C246.224 59.4067 242.422 58.2942 239.695 56.0692C236.969 53.7948 235.606 50.1112 235.606 45.0185V28.5538H230.257V18.0223H235.606V7.93573H249.055V18.0223H257.864V28.5538H249.055V45.1669C249.055 46.403 249.37 47.293 249.999 47.8368C250.628 48.3807 251.677 48.6527 253.145 48.6527H257.943Z" />
          <path className='fill-slate-900' d="M262.839 38.6403C262.839 34.3881 263.678 30.6551 265.356 27.4413C267.086 24.2274 269.42 21.7553 272.356 20.0247C275.292 18.2942 278.569 17.4289 282.188 17.4289C285.281 17.4289 287.982 18.0222 290.289 19.2089C292.648 20.3956 294.457 21.953 295.716 23.8813V18.0223H309.165V59.4067H295.716V53.5476C294.405 55.4759 292.57 57.0334 290.21 58.22C287.903 59.4067 285.203 60 282.109 60C278.543 60 275.292 59.1347 272.356 57.4042C269.42 55.6242 267.086 53.1273 265.356 49.9135C263.678 46.6502 262.839 42.8925 262.839 38.6403ZM295.716 38.7145C295.716 35.5501 294.772 33.0531 292.884 31.2237C291.049 29.3943 288.794 28.4796 286.12 28.4796C283.446 28.4796 281.165 29.3943 279.277 31.2237C277.442 33.0037 276.525 35.4759 276.525 38.6403C276.525 41.8047 277.442 44.3263 279.277 46.2052C281.165 48.0346 283.446 48.9493 286.12 48.9493C288.794 48.9493 291.049 48.0346 292.884 46.2052C294.772 44.3758 295.716 41.8789 295.716 38.7145Z" />
          <path className='fill-slate-900' d="M344.93 59.4067L336.514 47.911L329.435 59.4067H314.884L329.356 38.2695L314.491 18.0223H329.592L338.008 29.4438L345.087 18.0223H359.638L344.93 38.8628L360.031 59.4067H344.93Z" />
          <path d="M372.056 13.7206C369.696 13.7206 367.756 13.0779 366.236 11.7923C364.768 10.4574 364.033 8.82571 364.033 6.89741C364.033 4.91966 364.768 3.28801 366.236 2.00247C367.756 0.667492 369.696 0 372.056 0C374.363 0 376.251 0.667492 377.719 2.00247C379.24 3.28801 380 4.91966 380 6.89741C380 8.82571 379.24 10.4574 377.719 11.7923C376.251 13.0779 374.363 13.7206 372.056 13.7206ZM378.742 18.0223V59.4067H365.292V18.0223H378.742Z" />
          <path className='fill-lime-500' d="M203.91 47.54L213.427 18.022H227.742L212.169 59.4065H195.573L180 18.022H194.393L203.91 47.54Z" />
          <path className='fill-lime-500' fill-rule="evenodd" clip-rule="evenodd" d="M30 12C30 5.37258 35.3726 0 42 0H48C54.6274 0 60 5.37258 60 12V30H42C35.3726 30 30 24.6274 30 18V12ZM90 30H60V48C60 54.6274 65.3726 60 72 60H78C84.6274 60 90 54.6274 90 48V30ZM90 30V12C90 5.37258 95.3726 0 102 0H108C114.627 0 120 5.37258 120 12V18C120 24.6274 114.627 30 108 30H90Z" />
          <path className='fill-slate-900' d="M0 42C0 35.3726 5.37258 30 12 30H18C24.6274 30 30 35.3726 30 42V48C30 54.6274 24.6274 60 18 60H12C5.37258 60 0 54.6274 0 48V42Z" />
          <path className='fill-slate-900' d="M120 42C120 35.3726 125.373 30 132 30H138C144.627 30 150 35.3726 150 42V48C150 54.6274 144.627 60 138 60H132C125.373 60 120 54.6274 120 48V42Z" />
        </svg>



      </div>

      <Ride/>









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
              getValue(value, setDotAData)
            }
          }}
          onSelected={(option) => setDotState(prev => ({ ...prev, dotA: option }))}
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
              getValue(value, setDotBData)
            }
          }}
          onSelected={(option) => setDotState(prev => ({ ...prev, dotB: option }))}
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
