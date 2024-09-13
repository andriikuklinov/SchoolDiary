import { Dispatch, SetStateAction } from "react";
import OrderByItem from "./model/OrderByModel";
import OrderByModel from "./model/OrderByModel";

export default function buildOrderBySettings(settings: Array<{ "PropertyName": string, "Direction": string }>,
    setStateFunc: Dispatch<SetStateAction<{ "PropertyName": string, "Direction": string }[]>>, propertyName: string, direction: string) {
    let targetSetting = settings.find((val) => val.PropertyName == propertyName);
    if (direction != '') {
        if (targetSetting == undefined) {
            setStateFunc([...settings, { 'PropertyName': propertyName, 'Direction': direction }])
        }
        else {
            setStateFunc(settings.map((setting) => {
                if (setting.PropertyName == propertyName) {
                    setting.Direction = direction;
                    return setting;
                }
                return setting;
            }));
        }
    }
    else {
        setStateFunc(settings.filter((setting) => setting.Direction != ''));
    }
}