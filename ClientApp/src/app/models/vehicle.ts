import { KeyValuePair } from "./keyValuePair";
import { Contact } from "./contact";


export interface Vehicle{
    id:number;
    model:KeyValuePair;
    make:KeyValuePair;
    isRegistered:boolean;
    features:KeyValuePair[];
    contact:Contact;
    lastUpdate:string;


}

