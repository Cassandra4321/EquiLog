import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HorsesService } from '../../services/horse.service';
import { FormsModule } from '@angular/forms';
import {
  HorseDto,
  CreateHorseRequest,
  UpdateHorseRequest,
} from '../../domain/client';
import { FooterComponent } from '../../components/footer/footer.component';
import { HeaderComponent } from '../../components/header/header.component';

@Component({
  selector: 'app-my-horses-page',
  standalone: true,
  imports: [CommonModule, FormsModule, FooterComponent, HeaderComponent],
  templateUrl: './my-horses-page.component.html',
  styleUrls: ['./my-horses-page.component.scss'],
})
export class MyHorsesPageComponent implements OnInit {
  private horsesService = inject(HorsesService);
  myHorses: HorseDto[] = [];
  selectedHorse: HorseDto | null = null;
  isEditing = false;
  showForm = false;
  loading = true;
  error: string | null = null;

  private convertToUpdateRequest(horse: HorseDto): UpdateHorseRequest {
    const request = new UpdateHorseRequest();
    request.name = horse.name ?? '';
    request.age = horse.age ?? 0;
    request.imageUrl = horse.imageUrl ?? '';
    request.emergencyContactNumber = horse.emergencyContactNumber ?? '';
    request.coRiderName = horse.coRiderName;
    request.gender = horse.gender ?? '';
    request.hoofStatus = horse.hoofStatus ?? '';
    request.pasture = horse.pasture ?? '';
    request.blanket = horse.blanket;
    request.flyMask = horse.flyMask;
    request.boots = horse.boots;
    request.turnoutInstructions = horse.turnoutInstructions;
    request.intakeInstructions = horse.intakeInstructions;
    request.feedingInstructions = horse.feedingInstructions;
    request.otherInfo = horse.otherInfo;
    return request;
  }

  private convertToCreateRequest(horse: HorseDto): CreateHorseRequest {
    const request = new CreateHorseRequest();
    request.name = horse.name ?? '';
    request.age = horse.age ?? 0;
    request.imageUrl = horse.imageUrl ?? '';
    request.ownerId = horse.ownerId ?? '';
    request.emergencyContactNumber = horse.emergencyContactNumber ?? '';
    request.coRiderName = horse.coRiderName;
    request.gender = horse.gender ?? '';
    request.hoofStatus = horse.hoofStatus ?? '';
    request.pasture = horse.pasture ?? '';
    request.blanket = horse.blanket;
    request.flyMask = horse.flyMask;
    request.boots = horse.boots;
    request.turnoutInstructions = horse.turnoutInstructions;
    request.intakeInstructions = horse.intakeInstructions;
    request.feedingInstructions = horse.feedingInstructions;
    request.otherInfo = horse.otherInfo;
    return request;
  }

  ngOnInit(): void {
    this.loadMyHorses();
  }

  loadMyHorses(): void {
    this.loading = true;
    this.error = null;

    this.horsesService.getMyHorses().subscribe({
      next: horses => {
        this.myHorses = horses;
        this.loading = false;
      },
      error: error => {
        console.error('Error loading my horses: ', error);
        this.error = 'Kunde inte hämta dina hästar. Försök igen senare.';
        this.loading = false;
      },
    });
  }

  trackByHorseId(index: number, horse: HorseDto): unknown {
    return horse.id || index;
  }

  onImageError(event: Event): void {
    const target = event.target as HTMLImageElement;
    target.src = '/default-horse.png';
  }

  startAddHorse(): void {
    this.selectedHorse = null;
    this.isEditing = false;
    this.showForm = true;
    this.scrollToForm();
  }

  startEditHorse(horse: HorseDto): void {
    this.selectedHorse = HorseDto.fromJS(horse);
    this.isEditing = true;
    this.showForm = true;
    this.scrollToForm();
  }

  deleteHorse(horseId: number | undefined): void {
    if (!horseId || !confirm('Are you sure you want to delete this horse?'))
      return;

    this.horsesService.deleteHorse(horseId).subscribe({
      next: () => {
        this.myHorses = this.myHorses.filter(h => h.id !== horseId);
      },
      error: err => {
        console.error('Error deleting horse:', err);
        alert('Failed to delete horse');
      },
    });
  }

  private scrollToForm(): void {
    setTimeout(() => {
      document
        .getElementById('horse-form')
        ?.scrollIntoView({ behavior: 'smooth' });
    }, 0);
  }

  onSubmitHorse(): void {
    if (!this.selectedHorse) return;
    if (this.isEditing && this.selectedHorse.id) {
      const updateReq = this.convertToUpdateRequest(this.selectedHorse);
      this.horsesService
        .updateHorse(this.selectedHorse.id, updateReq)
        .subscribe({
          next: updated => {
            const index = this.myHorses.findIndex(h => h.id === updated.id);
            if (index !== -1) this.myHorses[index] = updated;
            this.showForm = false;
          },
          error: err => {
            console.error('Error updating horse:', err);
            alert('Failed to update horse');
          },
        });
    } else {
      const createReq = this.convertToCreateRequest(this.selectedHorse);
      this.horsesService.createHorse(createReq).subscribe({
        next: created => {
          this.myHorses.push(created);
          this.showForm = false;
        },
        error: err => {
          console.error('Error creating horse:', err);
          alert('Failed to create horse');
        },
      });
    }
  }
}
