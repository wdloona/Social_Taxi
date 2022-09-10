import { CheckerIcon } from "../../../CustomIcon";


const HeaderIcon = () => {
  return (
    <div className="w-80 bg-zinc-100 flex justify-center items-center">
      <div className="bg-white h-12 rounded-lg px-5 flex gap-x-3 items-center w-full mx-3 justify-center">
        <span>
          <CheckerIcon className="text-yellow-300 text-[32px]" />
        </span>
        <span className="text-yellow-300 text-[18px] font-bold">ТаксиВези</span>
      </div>
    </div>
  );
};

export default HeaderIcon;
