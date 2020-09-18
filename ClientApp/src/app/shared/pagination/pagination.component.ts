import { Component, OnInit ,Input } from '@angular/core';

@Component({
    selector: 'app-pagination',
    templateUrl:"pagination.component.html"
})

export class PaginationComponent implements OnInit {
    @Input() pageSize:number;
    @Input() totalItems:number;
    

    
    pagesNumber:number[] =[]
    
    
    test(){
        var pageNumber=this.totalItems / this.pageSize;

        console.log(pageNumber)
        console.log(this.totalItems)
        console.log(this.pageSize)
        for(var i = 1 ; i <= pageNumber;i++)
        {
            this.pagesNumber.push(i);
        }
        console.log(this.pagesNumber)
    }
    constructor() { }

    ngOnInit() { }
}