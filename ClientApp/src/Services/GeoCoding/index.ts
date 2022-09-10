import axios from "axios";

const key = `fd73680b2943440da30429ed980635af`;

const geoAxios = axios.create({
    baseURL:`https://api.opencagedata.com/geocode/v1`
})

type CountryMapT = {
    formatted:string,
    components:{
      city:string,
      country:string
      road:string
      house_number:string
    }
    geometry:{
        lat:number
        lng:number
    }
}
const geoCodingService = {
    getReverseCoding: async (lat:number,lng:number) => {
        const res = await geoAxios.get(`/json?key=${key}&q=${lat}+${lng}`)
        return res
    },
    getForwardCoding:async (text:string) => {
        const res = await geoAxios.get<{ results:Array<CountryMapT> }>(`/json?key=${key}&q=${text}&countrycode=ru&language=ru?abbrv=1&no_annotations=1`)
        return res.data
    }
}

export { geoCodingService }