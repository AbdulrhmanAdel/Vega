import { query } from '@angular/animations';
import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { SaveVehicle } from '../models/saveVehicle';
import { parseTemplate } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private readonly vehicleEndpoint ="/api/vehicles"
  constructor(private http:HttpClient) { }

  getMakes(){
    return this.http.get('/api/makes');
   
  }

  getFeatures(){
    return this.http.get("/api/features");
  }
  
  create(vehicle:SaveVehicle){
    return this.http.post(this.vehicleEndpoint,vehicle);
  }
  getVehicle(id){
    return this.http.get(this.vehicleEndpoint+"/"+id);
  }

  getVehicles(query){
    
    return this.http.get(this.vehicleEndpoint+"?"+this.toQueryString(query));
  }

  toQueryString(obj){
    var parts = []; 
    for(var prop in obj){
      var value = obj[prop];
      if (value != null && value != undefined) {
        parts.push(prop+"="+value);
      }
    }
    return parts.join('&');
  }

  update(v : SaveVehicle){
    return this.http.put(this.vehicleEndpoint+"/"+v.id ,v);
  }

  delete(id){
    return this.http.delete(this.vehicleEndpoint+"/" + id);
  }

  getChart(){
    return this.http.get("api/charts");
  }

  

}
