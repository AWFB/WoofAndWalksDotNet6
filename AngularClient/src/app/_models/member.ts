import { Photo } from "./Photo";

export interface Member {
  id: number;
  username: string;
  photoUrl: string;
  age: number;
  knownAs: string;
  created: Date;
  lastActive: Date;
  gender: string;
  introduction: string;
  nameOfDog: string;
  breedOfDog: string;
  city: string;
  country: string;
  photos: Photo[];
}


