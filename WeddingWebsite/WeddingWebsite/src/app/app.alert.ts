import { Component } from "@angular/core";
import { animate, state, style, transition, trigger } from "@angular/animations";

@Component({
    selector: "alert-message",
    templateUrl: "app.alert.html",
    animations: [

        trigger("thingState", [
            transition("* => void",

                animate('0.2s 0.1s ease-out', style({
                    opacity: 0,
                    transform: 'translateY(-100%)'
                })))
    ])]
})
export class AlertMessageComponent {
    alertType: string = "alert-info";
    alertMessage: string = null;
    alerts: Array<Alert>;

    constructor() {
        this.alerts = new Array<Alert>();
    }

    showMessage(type, message): void {
        var alert = new Alert ()
        alert.type = type;
        alert.message = message
        this.alerts.push(alert);
        // wait for 10 seconds, then get rid of it.
        setTimeout(() => { this.alerts.splice(this.alerts.indexOf(alert), 1); }, 10000);

    }
    error(message: string): void {
        this.showMessage("alert-danger", message);
    }

    success(message: string): void {
        this.showMessage("alert-success", message);
    }
}

export class Alert {
    type: string;
    message: string;
}