import { Button } from 'antd';
const Ride = () => {

    return (

        <div className="ride flex items-start flex-col gap-2">
            <div className="flex flex-row align-center gap-4">

                <div className="flex flex-row align-center gap-2">

                    <div className="text-base text-slate-500">
                        Дата и время:
                    </div>

                    <div className="text-lg text-slate-900">
                        10.10.2022г 8:00
                    </div>
                </div>

                <div className="flex flex-row align-center gap-2">

                    <div className="text-base text-slate-500">
                        Водитель:
                    </div>

                    <div className="text-lg text-slate-900">
                        Имя Водителя
                    </div>
                </div>
            </div>
            <div className="text-base text-slate-700">
                Описание. Lorem ipsum dolor sit amet consectetur adipisicing elit. Laudantium, repellendus nemo quo autem asperiores repudiandae ipsa cum id pariatur rem, nobis natus voluptas commodi fugiat numquam magnam aliquid doloribus consequuntur.
            </div>

            <Button type="primary" htmlType="submit" className='h-10 rounded-md bg-lime-500 text-lime-50 text-sm font-medium border-0'>
                Открыть чат
            </Button>

        </div>



    )
}

export default Ride;