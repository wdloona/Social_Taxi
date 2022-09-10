import { ReactNode, FC, useState, useEffect } from 'react'
import { AutoComplete, Input, Select } from 'antd'

type AutoCompleteInputT = {
    prefixInput?:ReactNode
    placeholder?:string
    options?:Array<{label:string,value:[number,number]}>
    defVal?:string
    onChange?:(value:string) => any
    onSelected?:(option:{label:string,value:[number,number]}) => any
}
const { Option } = Select

const AutoCompleteInput:FC<AutoCompleteInputT> = ({ prefixInput,placeholder,onChange,options,defVal,onSelected }) => {

    const [ v, setV ] = useState<Array<{key:string | number,label:string,value:[number,number]}> | undefined>(undefined)
    const [ inputValue, setInputValue ] = useState<string>("");

  useEffect(() => {
    console.log(inputValue);
  },[inputValue])

    useEffect(() => {
        console.log(options);
        const res = options?.map((e,k) => {
            return { ...e,key:k }
        })
        setV(res)
    },[options])

  return (
    // <AutoComplete 
    //     bordered={false}
    //     onSearch={(e:string) => {
    //         if(e.length >= 3){
    //             onChange && onChange(e)
    //         }
    //     }}
    //     options={v}
    // >
    //     <Input 
    //         className='h-10 border-0.5 rounded-lg border-b-2 border-zinc-200 hover:border-lime-300 ' 
    //         placeholder={placeholder} 
    //         prefix={prefixInput}
    //         value={inputValue}
    //         onChange={( { target:{ value } } ) => setInputValue(value)}
    //     />
    // </AutoComplete>
    <Select
      onSearch={(value) => {
        if(value.length >= 3){
          onChange && onChange(value)
        }
      }}
      value={v}
      
      placeholder={placeholder}
      suffixIcon={prefixInput}
      defaultActiveFirstOption={false}
      filterOption={false}
      showSearch
      showArrow={false}
      notFoundContent={null}
      onSelect={(value:any,option:any) => {
        console.log(option);
      }}
    >
      {
        v?.map((opt) => {
          return (
            <Option value={opt.value}>{opt.label}</Option>
          )
        })
      }
    </Select>
  )
}

export default AutoCompleteInput