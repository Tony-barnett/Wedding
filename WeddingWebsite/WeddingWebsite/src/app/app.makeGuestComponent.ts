import { Component, Input } from "@angular/core";

import { GuestService } from "app/GuestService";
import { Guest } from "app/Guest";
import { AddedGuestsComponent } from "app/app.addedGuestComponent";

@Component({
    selector: 'new-guest',
    template: `
    <div class="container">
        <form #guestForm="ngForm">
            <div>
                <label for="first-name">First name </label>
                <input [(ngModel)]="guest.firstName" name="first-name" placeholder="First Name" required/>

            </div>
            <div class="form-group">
                <label for="surname">Surname </label>
                <input [(ngModel)]="guest.surname" name="surname" placeholder="Surame" required/>
        
            </div>
            <div class="form-group">
                <label for="allergies">Allergies </label>
                <input [(ngModel)]="guest.allergies" name="allergies" placeholder="Allergies"/>
        
            </div>
            <div class="form-group">
                <label>Under 18?</label>
                <input type="checkbox" name="child" [(ngModel)]="guest.isChild"/>
        
            </div>
            <div class="form-group">
                <label>Under 10?</label>
                <input type="checkbox" name="youngChild" [(ngModel)]="guest.isYoungChild"/>
        
            </div>
            <div class="form-group">
                <label>Under 3?</label>
                <input type="checkbox" name="baby" [(ngModel)]="guest.isBaby"/>
        
            </div>

            <button type="submit" (click)="onSubmit()" class="btn btn-success" [disabled]="!guestForm.form.valid">Submit</button>
        </form>
    </div>
    `
})
export class NewGuestComponent {
    constructor(
        private guestService: GuestService,
        private guestTable: AddedGuestsComponent
    ) {
        this.guest = new Guest;
    }

    @Input() guest: Guest;

    onSubmit() {
        this.guestService
            .addGuest(this.guest)
            .subscribe(
            result => this.handleAddGuestPost(result))
    };

    private handleAddGuestPost(result: boolean) {
        if (result == true) {
            this.updateTable()
        } else {
            this.errorOut();
        }

        this.guest = new Guest;
    };

    private updateTable(): void {
        this.guestTable.guests.push(this.guest);
    };

    private errorOut(): void {
        console.log("shit gone sour");
    };
};
