import { ToastrService } from 'ngx-toastr';
import { PhotoService } from './../services/photo.service';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { VehicleService } from './../services/vehicle.service';
import { Vehicle } from './../models/vehicle';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Route } from '@angular/compiler/src/core';

@Component({
  selector: 'app-view-vehicle',
  templateUrl: './view-vehicle.component.html',
  styleUrls: ['./view-vehicle.component.css']
})
export class ViewVehicleComponent implements OnInit {
id:number;
vehicle:Vehicle;
@ViewChild('fileInput',{static:false}) fileInput:ElementRef;
photos:any[];

  constructor(
    private vehicleService:VehicleService,
    private route:ActivatedRoute,
    private router:Router,
    private photoService:PhotoService,
    private toastr:ToastrService
  ) {
    route.params.subscribe(p => {
    this.id=+p['id'];
    if(isNaN(this.id) || this.id <= 0){
      router.navigate(['/vehicles'])
      return;
    }
    })
   }

  ngOnInit() {
    this.vehicleService.getVehicle(this.id).subscribe(vehicle => {
      this.vehicle = vehicle as Vehicle;
      },err =>{
        if(err.status == 404 ){
          this.router.navigate(['/vehicles'])
          return;
        }
      });
      this.photoService.getPhotos(this.id).subscribe(photos =>{
          this.photos =photos as any[]
        });
  }
  delete(){
    if(confirm("Are u Sure?"))
      this.vehicleService.delete(this.vehicle.id).subscribe(x => {
        this.router.navigate(["/vehicles"])
      });
  }

  uploadPhoto(){
    
    var nativeElement:HTMLInputElement=this.fileInput.nativeElement;
    this.photoService.upload(this.vehicle.id,nativeElement.files[0]).subscribe(photo =>{
      this.photos.push(photo)
    },err =>{
      this.toastr.error(err,"Error")
    });
  }
  

}
