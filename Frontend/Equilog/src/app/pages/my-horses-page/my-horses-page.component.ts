import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HorsesService } from '../../services/horse.service';
import { FormsModule } from '@angular/forms';
import { HorseDto } from '../../domain/client';
import { FooterComponent } from '../../components/footer/footer.component';
import { HeaderComponent } from '../../components/header/header.component';

@Component({
  selector: 'app-my-horses-page',
  imports: [CommonModule, FormsModule, FooterComponent, HeaderComponent],
  templateUrl: './my-horses-page.component.html',
  styleUrls: ['./my-horses-page.component.scss'],
})
export class MyHorsesPageComponent implements OnInit {
  private horsesService = inject(HorsesService);
  myHorses: HorseDto[] = [];
  loading = true;
  error: string | null = null;

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
}
