
import { VehicleService } from '../services/vehicle.service';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-admin',
    templateUrl: 'chart.component.html'
})

export class ChartComponent implements OnInit {
    chart:any = {};
    data={};
    constructor(private vehicleService:VehicleService) { 
    }

    

    ngOnInit() {
        this.vehicleService.getChart().subscribe(chart => {
            this.chart = chart;

            this.data={
            labels:this.chart.makes as string[],
            datasets:[{
            data:this.chart.makeVehicles as number[],
            backgroundColor:["#ff6384","#36a2eb","ffce66"]
        }]
            }

        });
        
     }

   
}

