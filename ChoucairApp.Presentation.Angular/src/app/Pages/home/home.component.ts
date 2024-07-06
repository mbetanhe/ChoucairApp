import { Component } from '@angular/core';
import { IndexComponent } from "../Tasks/index/index.component";


@Component({
    selector: 'app-home',
    standalone: true,
    templateUrl: './home.component.html',
    styleUrl: './home.component.css',
    imports: [IndexComponent]
})
export class HomeComponent {

    userName : string = localStorage.getItem("userName") || "";

}
