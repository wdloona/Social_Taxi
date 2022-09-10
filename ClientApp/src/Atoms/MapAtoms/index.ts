import { selector, atom } from 'recoil'

const infoAboutRideState = atom<{ dotA?:{ label:string,value:[number,number] },dotB?:{ label:string,value:[number,number] }}>({
    key:"InfoAboutRideState",
    default:{
        dotA:undefined,
        dotB:undefined
    }
})

export { infoAboutRideState }
