
import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";

import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';

import { Guest } from "app/Guest";



@Injectable()
export class GuestService {
    constructor(private http: Http) { }

    getGuests(): Observable<Guest[]> {
        return this.http
            .get("/RSVP/GetStoredGuests")
            .map(this.extract);
    }

    addGuest(guest: Guest): Observable<boolean> {
        return this.http
            .post("/RSVP/AddGuest", guest)
            .map(response => response.json() == "Success");
    }

    deleteGuest(guest: Guest): Observable<boolean> {
        return this.http
            .delete("/RSVP/RemoveGuest?guestId=" + guest.id)
            .map(response => response.json() == "Success");
    }

    private extract(response: Response) {
        let body = response.json();
        return body || {};
    }


}