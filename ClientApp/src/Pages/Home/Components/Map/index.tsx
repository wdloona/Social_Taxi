import { useState, useEffect } from 'react'
import L from 'leaflet'
import { MapContainer, TileLayer, useMap, ZoomControl } from 'react-leaflet'
import "leaflet-routing-machine";
import drawLocales from "leaflet-draw-locales";
import "leaflet/dist/leaflet.css";
drawLocales("ru");


const CreateMap = () => {
  const [position, setPosition] = useState(null);
  
  const _map = useMap();
  _map.setZoom(20);
  _map.flyTo([54.9044, 52.3154]);
  _map.addEventListener('click',(e) => {
    const { latlng:{lat,lng} } = e

  })
  L.control.zoom({
    position:'bottomright'
  }).addTo(_map)
  L.Routing.control({
    waypoints: [
      L.latLng(54.9044, 52.3154),
      L.latLng(54.9014, 52.3284),
    ]
  }).addTo(_map);
  return null;
};


const Map = (className:any) => {

  return (
    <MapContainer
        center={[54.9044, 52.3154]}
        zoom={20}
        scrollWheelZoom
        id='map-container'
        className={"w-full h-full " + className}
        placeholder={null}
        zoomControl={false}
        style={{position:'absolute'}}
      >
        <TileLayer url="http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
        <CreateMap />
    </MapContainer>
  )
}

export default Map