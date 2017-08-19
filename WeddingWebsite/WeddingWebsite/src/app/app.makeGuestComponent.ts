import { Component, Input } from "@angular/core";

import { GuestService, NewGuestObject } from "app/GuestService";
import { Guest } from "app/Guest";
import { AddedGuestsComponent } from "app/app.addedGuestComponent";

@Component({
    selector: 'new-guest',
    templateUrl: 'app.makeGuestComponent.html'
})
export class NewGuestComponent {
    constructor(
        private guestService: GuestService,
        private guestTable: AddedGuestsComponent
    ) {
        this.guest = new Guest;
    }

    @Input() guest: Guest;

    private hideAndShow(hideId: string, showId: string): void {
        document.getElementById(hideId).style.display = 'none';
        document.getElementById(showId).style.display = 'block';

    }

    onSubmit() {
        this.hideAndShow('submit-text', 'spinny-span');

        this.guestService
            .addGuest(this.guest)
            .subscribe(
            result => { this.hideAndShow('spinny-span', 'submit-text'); this.handleAddGuestPost(result) })
    };

    private handleAddGuestPost(result: NewGuestObject) {
        if (result.success == true) {
            this.guest.id = result.newId;
            this.updateTable();
        } else {
            this.errorOut();
        }

        this.guest = new Guest;
    };

    private updateTable(): void {
        this.guestTable.guests.push(this.guest);
        this.guestTable.alertMessage.success(this.guest.firstName + " " + this.guest.surname + " was successfully added.");
    };

    private errorOut(): void {
        console.log("shit gone sour");
        this.guestTable.alertMessage.error("Error, please try again");
    };
};
