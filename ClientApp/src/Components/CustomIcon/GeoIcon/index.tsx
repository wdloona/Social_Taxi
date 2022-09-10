import type { CustomIconComponentProps } from '@ant-design/icons/lib/components/Icon';
import Icon from '@ant-design/icons/lib/components/Icon'
import { geoSvg } from '../../Svgs'

const GeoIcon = (props: Partial<CustomIconComponentProps>) => {
  return (
    <Icon component={geoSvg} {...props}/>
  )
}

export default GeoIcon