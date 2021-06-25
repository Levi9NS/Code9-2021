import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Participant } from 'src/app/models/Participant';

@Component({
  selector: 'app-participant',
  templateUrl: './participant.component.html',
  styleUrls: ['./participant.component.css']
})
export class ParticipantComponent implements OnInit {
  participant : Participant;
  newFormGroup: FormGroup;
  submitted = false;
  constructor(private formBuilder: FormBuilder, public dialogRef: MatDialogRef<ParticipantComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Participant) { }

  ngOnInit() {
    this.createForm();
  }

  createForm(){
    this.participant=new Participant();
    this.newFormGroup=this.formBuilder.group({
      FirstName: [this.participant.FirstName, Validators.required],

      LastName:[this.participant.LastName, Validators.required],

      Email: [this.participant.Email, Validators.required],

      Password: [this.participant.Password, Validators.required],
    });
  }


  AddSurvey()
  {
    
    this.participant.FirstName=this.newFormGroup.controls.FirstName.value;
    this.participant.LastName=this.newFormGroup.controls.LastName.value;
    this.participant.Email=this.newFormGroup.controls.Email.value;
    this.participant.Password=this.newFormGroup.controls.Password.value;
  }


  OnSubmit(){
    this.submitted = true;

        // stop here if form is invalid
        if (this.newFormGroup.invalid) {
            return;
        }

        // display form values on success
        alert('SUCCESS!! :-)\n\n' + JSON.stringify(this.newFormGroup.value, null, 4));
    this.AddSurvey();
    this.data=this.participant;
    this.dialogRef.close(this.data);
  }

  get f() { return this.newFormGroup.controls; }
}
