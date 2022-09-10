import { useState, useEffect } from 'react'
import L from 'leaflet'
import { MapContainer, TileLayer, useMap } from 'react-leaflet'
import "leaflet-routing-machine";
import drawLocales from "leaflet-draw-locales";
import "leaflet/dist/leaflet.css";
drawLocales("ru");


const CreateMap = () => {
  const [position, setPosition] = useState(null);
  
  const _map = useMap();
  _map.setZoom(13);
  _map.flyTo([54.9044, 52.3154]);
  _map.addEventListener('click',(e) => {
    const { latlng:{lat,lng} } = e

  })
  L.Routing.control({
    waypoints: [
      L.latLng(54.9044, 52.3154),
      L.latLng(54.9014, 52.3184),
    ]
  }).addTo(_map);
  return null;
};


const Map = () => {

  return (
    <MapContainer
        center={[54.9044, 52.3154]}
        zoom={13}
        scrollWheelZoom
        id='map-container'
        className="w-[calc(100%_+_500px)] h-[calc(100%_+_200px)]"
        placeholder={null}
      >
        <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
        <CreateMap />
    </MapContainer>
  )
}

export default Map