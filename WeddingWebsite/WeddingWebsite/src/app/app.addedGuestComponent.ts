import { Component } from '@angular/core';

import { Observable } from "rxjs/Observable";

import { GuestService } from "app/GuestService";
import { Guest } from "app/Guest";
import { Router } from "@angular/router";
import 'rxjs/add/operator/debounceTime';
import { Subject } from "rxjs/Subject";
import 'rxjs/add/operator/distinctUntilChanged';

@Component({
    selector: 'added-guests',
    templateUrl: 'app.addedGuestComponent.html'
})
export class AddedGuestsComponent {
    constructor(
        private guestService: GuestService
    ){}

    guests: Array<Guest>;

    private firstNameChanged = new Subject<Guest>();
    private surnameChanged = new Subject<Guest>();
    private allergiesChanged = new Subject<Guest>();

    ngOnInit(): void {
        this.guestService
            .getGuests()
            .subscribe(
                guest => this.guests = guest,
                error => console.log('bar', error)
            );
        
        this.firstNameChanged
            .debounceTime(500)
            .subscribe(
            g => this.guestService
                .updateGuest(g)
                .subscribe(
                    result => {
                        if (result) {
                            this.guests[this.guests.indexOf(g)] = g;
                        } else {
                            console.log("whert");
                        }
                }));

        this.surnameChanged
            .debounceTime(500)
            .subscribe(
            g => this.guestService
                .updateGuest(g)
                .subscribe(
                result => {
                    if (result) {
                        this.guests[this.guests.indexOf(g)] = g;
                    } else {
                        console.log("whert");
                    }
                }));

        this.allergiesChanged
            .debounceTime(500)
            .subscribe(
            g => this.guestService
                .updateGuest(g)
                .subscribe(
                result => {
                    if (result) {
                        this.guests[this.guests.indexOf(g)] = g;
                    } else {
                        console.log("whert");
                    }
                }));
        
    };

    updateFirstName(guest: Guest, value: string): void {
        guest.firstName = value.trim();
        this.firstNameChanged.next(guest);

    };

    updateSurname(guest: Guest, value: string): void {
        guest.surname = value.trim();
        this.surnameChanged.next(guest);

    };

    updateAllergies(guest: Guest, value: string): void {
        guest.allergies = value.trim();
        this.allergiesChanged.next(guest);

    };

    editPart(showId: string, hideId: string, guestId: AAGUID): void{
        document.getElementById(hideId + "-" + guestId).style.display = "none";
        document.getElementById(showId + "-" + guestId).style.display = "block";
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