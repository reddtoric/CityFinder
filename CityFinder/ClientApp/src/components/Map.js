import React from 'react'
import { GoogleMap, useJsApiLoader, Marker } from '@react-google-maps/api';

const containerStyle = {
    width: '400px',
    height: '400px'
};

function Map({ lat, lng }) {
    const { isLoaded } = useJsApiLoader({
        id: 'google-map-script',
        googleMapsApiKey: "AIzaSyDa2Ihh0MNLmCqEnfsRPXdchZnL55q39P4"
    })

    const [, setMap] = React.useState(null)

    const onLoad = React.useCallback(function callback(map) {
        setMap(map)
    }, [])

    const onUnmount = React.useCallback(function callback(map) {
        setMap(null)
    }, [])

    return isLoaded ? (
        <GoogleMap
            mapContainerStyle={containerStyle}
            zoom={10}
            center={{ lat, lng }}
            onLoad={onLoad}
            onUnmount={onUnmount}
        >
            <Marker position={{ lat, lng }} />
            <></>
        </GoogleMap>
    ) : <></>
}

export default React.memo(Map)