import Icon from '@ant-design/icons/lib/components/Icon'
import type { CustomIconComponentProps } from '@ant-design/icons/lib/components/Icon';
import { Space } from 'antd';
import React from 'react'
import { checkerSvg } from '../../Svgs' 

const Checker = (props: Partial<CustomIconComponentProps>) => {
  return (
    <Icon component={checkerSvg} {...props}/>
  )
}

export default Checker