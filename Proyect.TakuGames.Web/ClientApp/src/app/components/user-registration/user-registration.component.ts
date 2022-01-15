import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { UserService } from '../../services/user.service';
import { SnackbarService } from '../../services/snackbar.service';
import { LoginComponent } from '../login/login.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.scss']
})
export class UserRegistrationComponent implements OnInit, OnDestroy {
  private unsubscribes$ = new Subject<void>();
  registerForm: FormGroup;
  submitted = false;
  private formData = new FormData();


  files;
  coverImagePath;

  constructor(
    private formBuilder: FormBuilder,
    private useService: UserService,
    private router: Router,
    private snackbarService: SnackbarService,
    public dialog: MatDialog) {
      this.buildForm()
    }

  ngOnInit():void {}
  ngOnDestroy(): void {
    this.unsubscribes$.next();
    this.unsubscribes$.complete();
  }
  uploadImage(event) {
    this.files = event.target.files;
    const reader = new FileReader();
    reader.readAsDataURL(event.target.files[0]);
    reader.onload = (myevent: ProgressEvent) => {
      this.coverImagePath = (myevent.target as FileReader).result;
    };
  }
  registerUser(event: Event): void {
    if (this.files && this.files.length > 0) {
      for (let i = 0; i < this.files.length; i++) {
        this.formData.append('file' + i, this.files[i]);
      }
    }
    this.formData.append('UserFormData', JSON.stringify(this.registerForm.value));
    // preventDefaultCancela comportamiento del html  que viene por defecto
    event.preventDefault();
    this.submitted = true;
    if (this.registerForm.valid) {
      this.useService.registerUser(this.formData)
        .pipe(takeUntil(this.unsubscribes$))
        .subscribe
        (
          (data) => {
            this.snackbarService.showSnackBar('El usuario se ha registrado con exito');
            this.router.navigate(['/']);
          }, error => {
            this.snackbarService.showSnackBar('Error ocurrido !! intentalo otra vez');
            this.registerForm.controls['userName'].setErrors({ 'incorrect': true});   
            console.log('Error ocurred while user register: ', error);          
          }
        );
    } else {
      this.registerForm.markAllAsTouched();
    }
  }

  openLoginDialog(): void {
    this.dialog.open(LoginComponent, {
      height: '770px',
    })
  };

  private buildForm(): void {
    this.registerForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      userName: ['', Validators.required],
      /*email: ['', [Validators.required, Validators.email]],*/
      gender: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
      check:[false, Validators.requiredTrue]
    }, {
      validator: this.MustMatch('password', 'confirmPassword')
    });
  }

  private MustMatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors.mustMatch) {
        // returna si otro validador ya ha encontrado un error en el matchingControl
        return;
      }
        // establecer error en matchingControl si falla la validación
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ mustMatch: true });
      } else {
        matchingControl.setErrors(null);
      }
    }
  }

    get firstNameField(): AbstractControl {
      return this.registerForm.get('firstName');
    }
    get lastname(): AbstractControl {
      return this.registerForm.get('lastName');
    }
    get username(): AbstractControl {
      return this.registerForm.get('userName');
    }
    get password(): AbstractControl {
      return this.registerForm.get('password');
    }
    get gender(): AbstractControl {
      return this.registerForm.get('gender');
    }
    get check(): AbstractControl {
      return this.registerForm.get('check');
    }
    get confirmPassword(): AbstractControl {
      return this.registerForm.get('confirmPassword');
    }
    get f() { return this.registerForm.controls; }
  
}
