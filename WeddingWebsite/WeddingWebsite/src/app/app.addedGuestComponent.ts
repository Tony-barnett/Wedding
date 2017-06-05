import { Component } from '@angular/core';

import { Observable } from "rxjs/Observable";

import { GuestService } from "app/GuestService";
import { Guest } from "app/Guest";

@Component({
    selector: 'added-guests',
    template: `
    <new-guest></new-guest>

    <table [class.table]="true" [class.table-striped]="true" *ngIf="guests?.length > 0">
        <thead>
            <tr>
                <th> Firstname </th>
                <th> Lastname </th> 
                <th> Allergies </th>
                <th> 11 - 18 years old </th>
                <th> 4 - 10 years old </th> 
                <th> under 4 years old </th>
                <th> </th>
            </tr>
        </thead>
        <tbody>
            <tr *ngFor="let guest of guests">
                <td> {{ guest.firstName }} </td>
                <td> {{ guest.surname }} </td>
                <td> {{ guest.allergies }} </td>
                <td> <span *ngIf="guest.isChild" class="fa fa-check"> </span> </td>
                <td> <span *ngIf="guest.isBaby" class="fa fa-check"> </span> </td>
                <td> <span *ngIf="guest.isComing" class="fa fa-check"> </span> </td>
                <td> <button class="btn btn-danger" (click)="onDelete(guest)"> <span class="fa fa-trash"> Delete </span> </button> </td>
            </tr>
        </tbody>
    </table>
    `
})
export class AddedGuestsComponent {
    constructor(
        private guestService: GuestService
    ){}

    guests: Array<Guest>;

    ngOnInit(): void {
        this.guestService
            .getGuests()
            .subscribe(
                guest => this.guests = guest,
                error => console.log('bar', error)
            );
    };

    onDelete(guest: Guest): void {
        this.guestService
            .deleteGuest(guest)
            .subscribe(
            result => {
                console.log(result);
                this.guestWasDeleted(result, guest);
            },
            error => console.log("couldn't do it cap'n", error)
            )
    };

    guestWasDeleted(yes: boolean, guest: Guest): void {
        if (yes) {
            var index = this.guests.indexOf(guest);
            console.log(index);
            console.log(guest);
            console.log(this.guests);
            this.guests.splice(index, 1);
        }
    }
};