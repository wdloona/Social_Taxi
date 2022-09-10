import { ReactNode, FC, useState, useEffect } from 'react'
import { AutoComplete, Input } from 'antd'

type AutoCompleteInputT = {
    prefixInput?:ReactNode
    placeholder?:string
    options?:Array<{label:string,value:[number,number]}>
    defVal?:string
    onChange?:(value:string) => any
    onSelected?:(option:{label:string,value:[number,number]}) => any
}

const AutoCompleteInput:FC<AutoCompleteInputT> = ({ prefixInput,placeholder,onChange,options,defVal,onSelected }) => {

    const [ v, setV ] = useState<Array<{key:string | number,label:string,value:[number,number]}> | undefined>(undefined)

    useEffect(() => {
        console.log(options);
        const res = options?.map((e,k) => {
            return { ...e,key:k }
        })
        setV(res)
    },[options])

  return (
    <AutoComplete 
        bordered={false}
        onSearch={(e:string) => {
            console.log(e)
            if(e.length >= 3){
                onChange && onChange(e)
            }
        }}
        options={v}
        onSelect={(value:any,option:{label:string,value:[number,number]}) => {
            console.log(option);
            onSelected && onSelected(option)
        }}
    >
        
    </AutoComplete>
  )
}

export default AutoCompleteInput